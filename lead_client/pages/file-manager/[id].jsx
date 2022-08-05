import {useEffect, useState} from "react";
import {useRouter} from "next/router";
import {useSession} from "next-auth/react";

export default function FileMatcher({}){

    const router = useRouter();
    const[FileId, setFileId] = useState(null);
    const[allDirectusCollections, setAllDirectusCollections] = useState(null);

    const { data: session, status } = useSession({
        required: true
    });

    const getDirectusCollections = async(setter) => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${session.user.accessToken}`,
            }
        };

        const result = await fetch("https://admin.istlift.com/collections",
                                        requestOptions
        )
            .then(resp=> resp.json())
            .then(res => {
                setter(res);
            });

    }

    useEffect(()=>{
        if(router.isReady)
            setFileId(router.query);
    },[router])

    useEffect(()=>{
        if(FileId && session){
            getDirectusCollections(setAllDirectusCollections);

        }
    },[FileId, session])

    useEffect(()=>{
        console.log(allDirectusCollections);
    },[allDirectusCollections])

    return(
        <>

        </>
    )
}