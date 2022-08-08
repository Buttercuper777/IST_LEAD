import {useEffect, useState} from "react";
import {useRouter} from "next/router";
import {useSession} from "next-auth/react";
import ModalMessage from "../../Components/Modal/ModalMessage";
import MatcherComp from "../../Components/FileManager/MatcherComp";

import styles from "../../styles/FilePage.module.css"
import {Matcher_AddFileId} from "../../helpers/ModelMathcer";

const LEAD_API = process.env.NEXT_PUBLIC_LEAD_API;
const Directus_API = process.env.NEXT_PUBLIC_DIRECTUS_API;

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

    useEffect(()=>{
        if(FileId && FileId !== null)
            Matcher_AddFileId(FileId.id);
    },[FileId])

    const getDirectusCollections = async(setter, statusSetter) => {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${session.user.accessToken}`,
            }
        };

        const result = await fetch(Directus_API + "/fields/Products",
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

        try {
            const result = await fetch(LEAD_API + "/api/Excel/HandleExcel?id=" + id,
                requestOptions
            )
                .then(res => {
                    statusSetter(res.status);
                    return res;
                })
                .then(res => res.json())
                .then(res => {
                    setter(res);
                });
        }catch(ex){
            statusSetter(ex.message);
            setter(ex);
        }
    }

    useEffect(()=>{
        if(router.isReady)
            setFileId(router.query);
    },[router])

    useEffect(()=>{
        if(FileId && session){
            getDirectusCollections(setAllDirectusCollections, setApiRespCode);
            getExcelColumns(FileId.id, setExcelColumns, setApiRespCode);

        }
    },[FileId, session])

    useEffect(()=>{
        if(allDirectusCollections && allDirectusCollections.errors){
            console.log("Directus errors: ", allDirectusCollections.errors[0]);
            setErrors(allDirectusCollections.errors[0]);
        }
        console.log(allDirectusCollections);
    },[allDirectusCollections])

    useEffect(()=>{
        if(excelColumns && excelColumns.name === "TypeError"){
            setErrors({message: excelColumns.message + ' \n ' + excelColumns.stack});
        }
        if(excelColumns){
            console.log(excelColumns);
        }
    },[excelColumns])

    return (PageErrors === null) ? (
        <>
            <div className={styles.chooseMatcherBlock}>
                {PageErrors === null
                && excelColumns
                && excelColumns.name !== "TypeError" ?

                    excelColumns.columns.map((el, i) => (
                    <MatcherComp title={el.title}
                                 helpers={el.Helpers}
                                 collection={allDirectusCollections}
                                 location={el.Location}
                                 key={el.title}/>
                    )
                ): null}
            </div>
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