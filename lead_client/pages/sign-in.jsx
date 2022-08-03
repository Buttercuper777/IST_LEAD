import styles from "../styles/authPage.module.css"

import {getCsrfToken, signIn, useSession} from "next-auth/react";
import {useRouter} from "next/router";
import {useEffect, useState} from "react";

export default function SignIn({csrfToken}){

    const router = useRouter();
    const [error, setError] = useState(false);

    const {data: session, status} = useSession();

    useEffect(()=>{
       if(status === "authenticated")
           router.push('/');
    },[status])


    const handleSubmit = async (e) => {
        e.preventDefault();

        const res = await signIn('credentials', {
            redirect: false,
            email: e.target.email.value,
            password: e.target.password.value,
            callbackUrl: `/`,
        });

        if (res?.error) {
            setError(true);
        } else {
            router.push('/');
        }

    }

    return (
        <>
            <div className={styles.authFormContainer}>
                <form className={styles.authForm} noValidate onSubmit={(e) => handleSubmit(e)}>
                    <input name="csrfToken" type="hidden" defaultValue={csrfToken} />

                    <label htmlFor="email-address">
                        Email address:
                    </label>
                    <input
                        id="email-address"
                        name="email"
                        type="email"
                        autoComplete="email"
                        required
                        placeholder="Email address"
                    />

                    <label htmlFor="password">
                        Password:
                    </label>
                    <input
                        id="password"
                        name="password"
                        type="password"
                        autoComplete="password"
                        required
                        placeholder="Password"
                    />

                    <button type="submit">
                        Sign In
                    </button>

                    {error &&
                    <div className={styles.warning}>
                        Wrong email or password
                    </div>}

                </form>
            </div>

        </>

    );
}
export async function getServerSideProps(context) {
    return {
        props: {
            csrfToken: await getCsrfToken(context),
        },
    };
}
