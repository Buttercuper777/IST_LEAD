import '../styles/globals.css'
import {SessionProvider} from "next-auth/react";
import MainHeader from "../Components/MainHeader";
import Layout from "../Components/Layout";
import LogOutBtn from "../Components/LogOutBtn";

function MyApp({ Component, pageProps:{session, ...pageProps} }) {
  return(
      <>

      <SessionProvider session={session}>
          <Layout>
            <Component {...pageProps} />
          </Layout>
          <LogOutBtn/>
      </SessionProvider>
      </>
  )
}

export default MyApp
