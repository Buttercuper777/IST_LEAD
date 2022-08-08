const MatchedFields = {

    FileId: null,
    Fields: []

}

function Matcher_ElemEx(arr, item){

    let isChanged = false;

    arr.forEach((element, index) => {
        if((element.location.row === item.location.row) &&
            (element.location.col === item.location.col)) {
            arr[index] = item;
            isChanged = true;
        }
    });

    return isChanged;
}

export function Matcher_GetMatched(){
    return MatchedFields;
}

export function Matcher_AddFileId(FileId){
    MatchedFields.FileId = FileId;
    // console.warn("MATCHER: ", MatchedFields);
}

export function Matcher_AddNewField(f_name, location){
    if(typeof location === 'object' && location !== null){

        const newField = {
            field_name: f_name,
            location: location
        }

        if(MatchedFields.Fields.length > 0){
            if(!Matcher_ElemEx(MatchedFields.Fields, newField)){
                MatchedFields.Fields.push(newField);
            }

            console.log("MATCHER: ", MatchedFields);
        }
        else{
            MatchedFields.Fields.push(newField);
            console.log("MATCHER: ", MatchedFields);
        }
    }
}

export function Matcher_sameFinder(f_name){
    let res = false;

    MatchedFields.Fields.forEach((elem, i) =>{
        if(elem.field_name === f_name)
            res = true;
    })

    return res;
}






