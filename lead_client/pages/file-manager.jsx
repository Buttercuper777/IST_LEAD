import DragDropComponent from "../Components/FileManager/drag-drop";
import styles from "../styles/fileManagerPage.module.css"

export default function FileManager(){



    return(
        <>
            <div className={"row"}>
                <div className={"page-container"}>
                    <div className={"col-lg-6"}>
                        <h1 className={"PageCaption"}>Менеджер продуктов</h1>
                        <p className={"PageSubTitle"}>“Перетащите” нужный файл на эту страницу или
                            найдите его на вашем компьютере, нажав на надпись
                            “Загрузить файл”</p>
                        <div className={styles.DragDropBlock}>
                            <DragDropComponent/>
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}