import styles from "../../styles/modalMessage.module.css"

export default function ModalMessage({title, message, state, stateSetter}){
    return state ? (
        <>
            <div className={"row justify-content-center align-content-center"}
            style={{
                minHeight:"inherit",
            }}>
                <div className={"col-lg-6"}>
                    <div className={styles.ModalMessage}>
                        <div className={styles.TitleZone}>
                            <p>
                                {title}
                            </p>
                        </div>
                        <div className={styles.MessageZone}>
                            <p>
                                {message}
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </>
    ):null
}