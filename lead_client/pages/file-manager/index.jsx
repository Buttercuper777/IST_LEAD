import DragDropComponent from "../../Components/FileManager/drag-drop";
import styles from "../../styles/fileManagerPage.module.css"
import {useEffect, useState} from "react";
import {TheFile,FileName} from "../../Boilers/uploadFile";
import UploadedFile from "../../Components/FileManager/UploadedFile";
import sendFile from "../../helpers/sendFileHelper";
import {GuidCheck} from "../../helpers/GuidCheck";
import {useRouter} from "next/router";


export default function Index(){

    const[file, satFile] = useState(null);
    const[loadedFileVisible, setLoadedVisible] = useState(false);
    const[newFileId, setNewFileId] = useState("");
    const[savedFile, setSavedFile] = useState(null);

    const router = useRouter();

    useEffect(()=> {
        if(file && file !== null){
            setLoadedVisible(true);
        }
    },[file])

    const fileSender = () => {
        const res = sendFile(file, {
            variables : {}
        }).then()


        res.then(data =>{
            const id = data;
            setNewFileId(id);
        })

    }

    useEffect(()=>{
        if(newFileId && newFileId.length > 0){
            console.log(newFileId);
            router.push("/file-manager/" + newFileId);
        }
    },[newFileId])

    return(
        <>

            {/*<button onClick={()=>{setNewFileId(!newFileId)}}></button>*/}
            <div className={"row"}>

                <div className={"col-lg-6"}>
                    <h1 className={"PageCaption"}>Менеджер продуктов</h1>
                    <p className={"PageSubTitle"}>“Перетащите” нужный файл на эту страницу или
                        найдите его на вашем компьютере, нажав на надпись
                        “Загрузить файл”</p>
                    <div className={styles.DragDropBlock}>
                        <DragDropComponent returnFile={satFile}/>
                    </div>
                    <div className={styles.FileForSendBlock}>

                        <UploadedFile
                            name={file ? file.get(FileName) : null}
                            file={file ? file.get(TheFile) : null}
                            sendFnc={fileSender}
                            isVisible={file ? true: false}
                            // isVisible={true}
                            savedData={savedFile}
                        />

                    </div>
                </div>

            </div>
        </>
    )
}