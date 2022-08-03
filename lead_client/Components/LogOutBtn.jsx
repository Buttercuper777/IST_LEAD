import {useEffect, useState} from "react";
import {signOut, useSession} from "next-auth/react";
import styles from "../styles/LogOutBtn.module.css"


export default function LogOutBtn(){

const[authStatus, setAuth] = useState(null);
const {data: session, status} = useSession();

useEffect(()=>{
    setAuth(status);
},[status]);

function logOut(){
    signOut({ callbackUrl: '/' })
}

return authStatus !== "authenticated" ? (
    <>
        <div className={styles.LogOutBtn}>
            <p>{status}</p>
        </div>
    </>
    ) : authStatus === "authenticated" ? (
        <>
            <div className={styles.LogOutBtn}>
                <button className={styles.mainBtn} onClick={()=> logOut()}>
                    <img src="./images/log_out_icon.svg" style={{
                        marginBottom: 5 + "px"
                    }}/>
                    LogOut
                </button>
            </div>
        </>
    ) : (
    <>
        <div className={"LogOutBtn"}>
            <p>Auth status error!</p>
        </div>
    </>
);
}