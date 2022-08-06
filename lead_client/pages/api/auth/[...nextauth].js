const DirectusAPI = process.env.DIRECTUS_API
const JWT_Secret = process.env.JWT_SECRET

import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";

const refreshAccessToken = async(r_token) =>{
    console.log(Date.now(), " ", r_token)
    try {
        const url = DirectusAPI + '/auth/refresh'
        let token = "";

        if(r_token){
            token = r_token.refreshToken;
        }

        const payload = {
            "refresh_token": token
        };

        const response = await fetch(url, {
            headers: {
                'Content-Type': 'application/json',

            },
            method: "POST",
            body: JSON.stringify(payload),
        })

        const refreshedTokens = await response.json()

        if (!response.ok) {
            throw refreshedTokens
        }

        return {
            ...r_token,
            accessToken: refreshedTokens.data.access_token,
            accessTokenExpires: Date.now() + refreshedTokens.data.expires,
            refreshToken: refreshedTokens.refresh_token ?? r_token.refreshToken, // Fall back to old refresh token
        }
    } catch (error) {
        console.log(error)

        return {
            ...r_token,
            error: "RefreshAccessTokenError",
        }
    }
}


const options = {
    providers:[
        CredentialsProvider({
            name: 'Credentials',
            credentials: {
                email: {label: 'Email', type:'Text', placeholder: 'Enter your Email'},
                password: {label: 'Password', type:'Password', placeholder: 'Enter your password'},
            },
            async authorize(credentials){
                const payload = {
                    email: credentials.email,
                    password: credentials.password,
                };


            const res = await fetch(DirectusAPI + '/auth/login',{
                method: 'POST',
                body: JSON.stringify(payload),
                headers: {
                    'Content-Type': 'application/json',
                    'Accept-Language': 'en-US',
                },
            });

            const user = await res.json();
            // console.log(DirectusAPI);

            if(!res.ok)
                throw new Error('Wrong username or password');

            if(res.ok && user){
                return user;
            }

            return null;

            }
        }),
    ],

    session: {
        jwt: true
    },
    jwt:{
        secret: JWT_Secret,
    },

    callbacks: {
        async jwt({token, user, account}){
            if(user && account){
                console.log("exp: ", )
                return {
                    ...token,
                    accessToken: user.data.access_token,
                    refreshToken: user.data.refresh_token,
                    accessTokenExpires: Date.now() + user.data.expires,
                };

            }

            if(Date.now() < token.accessTokenExpires){
                // console.log(token.accessTokenExpires, Date.now())
                return token;
            }

            return refreshAccessToken(token)


        },


        async session({session, token}){
            session.user.accessToken = token.accessToken;
            session.user.refreshToken = token.refreshToken;

            return session;
        },
    },

    pages:{
        signIn: '/sign-in',
    }
}

export default (req, res) => NextAuth(req, res, options);
