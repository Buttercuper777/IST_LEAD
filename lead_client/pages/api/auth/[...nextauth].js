const DirectusAPI = process.env.DIRECTUS_API
const JWT_Secret = process.env.JWT_SECRET

import NextAuth from "next-auth";
import CredentialsProvider from "next-auth/providers/credentials";


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
        secret: JWT_Secret
    },

    callbacks: {
        async jwt({token, user, account}){
            if(user && account){
                return {
                    ...token,
                    accessToken: user.data.access_token,
                    refreshToken: user.data.refresh_token,
                };
            }

            return token;
        },


        async session({session, token}){
            session.user.accessToken = token.access_token;
            session.user.refreshToken = token.refreshToken;

            return session;
        },
    },
    pages:{
        signIn: '/sign-in',
    }
}

export default (req, res) => NextAuth(req, res, options);
