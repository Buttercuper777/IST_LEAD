import {useCallback, useReducer} from "react";
import {useDropzone} from "react-dropzone";
import styles from "../../styles/drag-drop.module.css"

export default function DragDropComponent(){

    // const LoadFile = "LoadFile"
    //
    // const[fileState, dispatch] = useReducer(reduser, {
    //     fileLoad: false,
    //     fileName: ""
    // });
    //
    // // ACTION GENERATOR: LoadFile
    // const LoadFileGenerator = (payload) => ({
    //     type: LoadFile,
    //     payload,
    // });
    //
    // // LOADED FILE DATA
    // function reduser(state, action){
    //     switch(action.type){
    //         case LoadFile:
    //             return{
    //                 fileLoad: true,
    //                 fileName: action.payload
    //             }
    //     }
    // }

    const onDrop = useCallback((acceptedFiles, rejectedFiles)=> {


        if(acceptedFiles && acceptedFiles.length > 0){
            const ex = acceptedFiles[0].name.split('.')[1];

            if(ex === 'xlsx'){
                console.log(true + ex);
            }
            else{
                alert("Доступны для обработки только Excel файлы (.xlsx)");
            }
        }

        let ErrorMsg = "";
        if(rejectedFiles && rejectedFiles.length > 1){
            alert("Too many files :( \nYou can only upload 1 (.xlsx) file!");
        }

        if(rejectedFiles && rejectedFiles.length > 0){
            rejectedFiles.forEach(file => {
                ErrorMsg += file.file.name + ": " + file.errors[0].message + "\n"
            });
            alert(ErrorMsg);
        }
    }, [])


    const {getRootProps, getInputProps, isDragActive} = useDropzone({onDrop,
        multiple:false
    });


    return (
        <>
            <div {...getRootProps()}>
                <input {...getInputProps()} />
                    <div className={"def-btn-l" + " " + styles.dropZone}>

                            <div className={isDragActive ?
                                styles.dropImage + " " + styles.active :
                                styles.dropImage}/>

                            {
                                isDragActive ?
                                    <p>...</p>:
                                    <p>
                                        <span>
                                            .xlsx
                                        </span>
                                            only
                                    </p>
                            }

                            <a>Загрузить файл</a>
                    </div>
            </div>
        </>
    )
}