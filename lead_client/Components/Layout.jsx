import MainHeader from "./MainHeader";
import {useRouter} from "next/router";
import {useSession} from "next-auth/react";
import {useEffect} from "react";

export default function Layout({ children }) {

    const router = useRouter();
    const {data: session, status} = useSession({
        required: true,
        onUnauthenticated() {
            if(router.asPath !== '/sign-in')
                router.push('/sign-in');
        },
    });


    return (
        <>
            <MainHeader/>
                <main>
                    <div className={"container"}>
                        {children}
                    </div>
                </main>

        </>
    )
}