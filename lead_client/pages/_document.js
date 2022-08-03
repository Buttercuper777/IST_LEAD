import { Html, Head, Main, NextScript } from 'next/document'
import MainHeader from "../Components/MainHeader";

export default function Document() {
    return (
        <Html>
            <Head>
                <link rel="stylesheet" href="/Bootstrap/bootstrap-grid.css"/>
                <link rel="preconnect" href="https://fonts.googleapis.com"/>
                <link rel="preconnect" href="https://fonts.gstatic.com"/>
                <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap"
                rel="stylesheet"/>
            </Head>
            <body>
                <Main />
                <NextScript />
            </body>
        </Html>
    )
}