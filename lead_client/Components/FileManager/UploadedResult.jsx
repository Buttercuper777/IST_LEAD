import styles from "../../styles/drag-drop.module.css";


export default function UploadedResult({isVisible, data}){
    return(
        <div className={isVisible ? styles.fileUploaded_show : styles.fileUploaded_hide}>

            <div className={"def-btn" + " " + styles.fileUploadedInfo}>
                <div className={styles.fileUploaded_image}>
                    <img src={"./images/xlsx_file.svg"}/>
                </div>

                <div>
                    <p className={styles.fileUploaded_title}>
                        {name ? name : "Ошибка при получении имени файл!"}
                    </p>

                    <ul className={styles.fileUploaded_fileParams}>
                        {file ? (
                            <>
                                <li><a>Размер файла: {Math.round(file.size / 1024)} Мб</a></li>
                                <li><a>Последние изменения: {(file.lastModifiedDate).toDateString()}</a></li>
                            </>
                        ): null}
                    </ul>
                </div>
            </div>

            {
                sendFnc && typeof sendFnc === 'function' && file && name ? (
                        <>
                            <button
                                onClick={(e)=>sendFnc(e)}
                                className={"sub-btn" + " " + styles.fileUploaded_sendBtn}
                            >
                                Отправить
                            </button>
                        </>
                    ) :
                    "Ошибка в приложении. Обратитесь сюда: https://t.me/maa_xim/"
            }


        </div>
    )
}