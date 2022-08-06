import {useEffect, useState} from "react";
import {useRouter} from "next/router";
import {useSession} from "next-auth/react";
import styles from "../../styles/FilePage.module.css"
import ModalMessage from "../../Components/Modal/ModalMessage";

const LEAD_API = process.env.NEXT_PUBLIC_LEAD_API;


export default function FileMatcher({}){

    const router = useRouter();
    const[FileId, setFileId] = useState(null);

    const[allDirectusCollections, setAllDirectusCollections] = useState(null);
    const[excelColumns, setExcelColumns] = useState(null);

    const[PageErrors, setErrors] = useState(null);
    const[apiRespCode, setApiRespCode] = useState("code");

    const { data: session, status } = useSession({
        required: true
    });

    const getDirectusCollections = async(setter, statusSetter) => {
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
            .then(res=> {
                statusSetter(res.status);
                return res;
            })
            .then(res => res.json())
            .then(res => {
                setter(res);
            });
    }

    const getExcelColumns = async (id, setter, statusSetter) => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        };

        const result = await fetch(LEAD_API + "/api/Excel/HandleExcel?id=" + id,
            requestOptions
        )
            .then(res=> {
                statusSetter(res.status);
                return res;
            })
            .then(res => res.json())
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
            getDirectusCollections(setAllDirectusCollections, setApiRespCode);
            // getExcelColumns(FileId.id, setExcelColumns, setApiRespCode);

        }
    },[FileId, session])

    useEffect(()=>{
        if(allDirectusCollections && allDirectusCollections.errors){
            console.log("Directus errors: ", allDirectusCollections.errors[0]);
            setErrors(allDirectusCollections.errors[0]);
        }
        // console.log(allDirectusCollections);
    },[allDirectusCollections])

    useEffect(()=>{
        if(excelColumns && excelColumns.errors){
            console.log("Excel errors: ", excelColumns.errors[0]);
            setErrors(excelColumns.errors[0]);
        }
        if(excelColumns){
            console.log(excelColumns);
        }
        // console.log(allDirectusCollections);
    },[excelColumns])

    return PageErrors === null ? (
        <>

        </>
    ):
        (
            <>
                <ModalMessage state={true}
                              message={PageErrors.message}
                              title={apiRespCode}
                />
            </>
        )
}