import {useEffect, useState} from "react";
import jsonExample from "./creationExample.json" assert {type: 'JSON'};
import {assign} from "next/dist/shared/lib/router/utils/querystring";

export default function ProductCreation(){

    const [jsonObj, setJson] = useState(null);

    const GetCategoryArrays = (object) => {
        let Arrays = new Array();

        for(let key in object){
            Array.isArray(object[key]) ? object[key].map(
                elem => {
                    if(elem.Id !== undefined && elem.Name !== undefined){
                        Arrays.push(Object.assign(elem, {"category_type": key}));
                    }
                }
            ) : null;
        }
        return Arrays;
    }


    useEffect(()=>{
        setJson(jsonExample);
    }, []);

    return(
        <>
            <div className={"row"}>
                <div className={"col-6"}>
                    sfsdc
                </div>
                <div className={"col-6"}>
                    <p>Таких категорий ещё не существует:</p>
                    <ul>
                        {jsonObj !== null ? jsonObj.map(elem => {
                            let elemArrays = GetArrays(elem);
                            if(elemArrays.length > 0){
                                elemArrays.map(item => {
                                    if(item.Id === 0){

                                    }
                                        // console.log("Not Exist: ", elem.product_name_ru,"\n", item.category_type, item.Name);
                                })
                            }
                        }) : null}
                    </ul>
                </div>
            </div>
        </>
    )
}