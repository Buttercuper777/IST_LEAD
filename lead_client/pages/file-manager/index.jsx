import DragDropComponent from "../../Components/FileManager/drag-drop";
import styles from "../../styles/fileManagerPage.module.css"
import {useEffect, useState} from "react";
import {TheFile,FileName} from "../../Boilers/uploadFile";
import UploadedFile from "../../Components/FileManager/UploadedFile";
import sendFile from "../../helpers/sendFileHelper";
import {GuidCheck} from "../../helpers/GuidCheck";


export default function Index(){

    const[file, satFile] = useState(null);
    const[loadedFileVisible, setLoadedVisible] = useState(false);
    const[newFileId, setNewFileId] = useState(null);
    const[savedFile, setSavedFile] = useState(null);

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

    // useEffect(()=>{
    //         fetch("https://localhost:5001/api/Excel/GetJsonModel?id=3d406179-4ab9-49fa-b854-ca6de7f5f3fb").then(
    //             res => res.json().then(res => console.log(res))
    //         )
    //     if(GuidCheck.test(newFileId)){
    //     }
    // },[newFileId])

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