import styles from "../../styles/drag-drop.module.css"

export default function UploadedFile({isVisible, name, file, sendFnc, savedData}){

    //Styles in DragDrop css
    return(
        <div className={isVisible ? styles.fileUploaded_show : styles.fileUploaded_hide}>

            <div className={"def-btn" + " " + styles.fileUploadedInfo}>
                    <div className={styles.fileUploaded_image}>
                        <img src={"./images/xlsx_file.svg"}/>
                    </div>

                    <div>


                        {(savedData === null) ?
                            (
                                <p className={styles.fileUploaded_title}>
                                    {name ? name : "Ошибка при получении имени файла!"}
                                </p>
                            ): (savedData !== undefined && savedData !== null) ?
                            (
                                <p className={styles.fileUploaded_title}>
                                    Saved file
                                </p>
                            ):
                            (
                                <p className={styles.fileUploaded_title}>
                                    Ошибка в приложении. Обратитесь сюда: https://t.me/maa_xim/
                                </p>
                            )
                        }



                        <ul className={styles.fileUploaded_fileParams}>

                            {(savedData === null && file) ?
                                (
                                    <>
                                        <li><a>Размер файла: {Math.round(file.size / 1024)} Мб</a></li>
                                        <li><a>Последние изменения: {(file.lastModifiedDate).toDateString()}</a></li>
                                    </>
                                ):(savedData !== undefined && savedData !== null) ? (
                                    <>
                                        Saved params
                                    </>
                                ):null
                            }

                        </ul>
                    </div>
            </div>

            {
                sendFnc && typeof sendFnc === 'function' && file && name && savedData === null ? (
                    <>
                        <button
                            onClick={(e)=>sendFnc(e)}
                            className={"sub-btn" + " " + styles.fileUploaded_sendBtn}
                        >
                            Отправить
                        </button>
                    </>
                ) : (savedData !== undefined && savedData !== null) ? null:
                "Ошибка в приложении. Обратитесь сюда: https://t.me/maa_xim/"
            }


        </div>
    )
}