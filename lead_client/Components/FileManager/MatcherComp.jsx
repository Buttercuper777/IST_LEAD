import styles from "../../styles/matchChooser.module.css";
import {
    Matcher_AddFileId,
    Matcher_AddNewField,
    Matcher_GetMatched,
    Matcher_sameFinder
} from "../../helpers/ModelMathcer";
import {useEffect, useState} from "react";
import ModalMessage from "../Modal/ModalMessage";

export default function MatcherComp({title, helpers, collection, location}){

    const[isRepeat, setRepeat] = useState(null);

    const[showFinal, setShowFinal] = useState(null);


    const handleChoose = (e) =>{
        if(Matcher_sameFinder(e.target.value)){
            setRepeat(true);
        }
        else
            Matcher_AddNewField(e.target.value, location);

        const obj = Matcher_GetMatched();
        if(obj && obj.Fields.length === 14){
            setShowFinal(JSON.stringify(obj));
        }

    }

    useEffect(()=>{
        if(showFinal){
            const doc = document.querySelectorAll(`.${styles.matcherBlock}`);
            console.log(doc);

            doc.forEach(el => {
                el.classList.add(`${styles.active}`);
            });

            // doc.classList.add(`${styles.active}`);
        }
    },[showFinal])

    useEffect(()=>{
        if(isRepeat){
            console.error("Repeat!")
        }
    },[isRepeat])

    return !showFinal ? (
        <>
            <div className={styles.matcherBlock}>
                <p className={styles.matcherTitle}>{title ? title : "Error"}</p>
                <ul className={styles.helpersList}>
                    <p className={styles.helpersTitle}>{helpers ? ("Значения в таблице:"):("Error")}</p>
                    {helpers ? helpers.map((el, i) =>
                        (
                            <li key={el + i}><a>{el}</a></li>
                        )
                    ) : (
                        <li><a>Error</a></li>
                    )}
                </ul>
                <span className={styles.customDropdown}>
                    <select
                        onChange={(e) => handleChoose(e)}
                        defaultValue='Choose'
                    >
                        <option disabled value='Choose'>
                            Choose
                        </option>
                        {
                            collection && !(collection.error) && collection.data ? (
                                collection.data.map((el, i)=>{
                                    return (
                                        <option
                                            key={el.field + i}
                                            value={el.field}
                                        >
                                            {el.meta.translations ? (
                                                el.meta.translations[0].translation + " - "
                                            ) : null}
                                            {el.field}

                                        </option>
                                    )
                                })
                            ): <option>Ошибка при получении модели</option>
                        }
                    </select>
                </span>
            </div>
        </>
    ) : (

            <ModalMessage
                state={showFinal ? true : false}
                title={"Это сообщение только для презентации"}
                message={showFinal}
            />


    )
}