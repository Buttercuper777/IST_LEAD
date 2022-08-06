const LEAD_API = process.env.NEXT_PUBLIC_LEAD_API
const SEND_FILE_API_BP = LEAD_API + "/api/Files"


const sendFile = async (query, {variables = {}}) => {

    const res = await fetch(SEND_FILE_API_BP, {
        method: 'POST',
        body: query
    })

    const json = await res.json()

    if (json.errors) {
        // throw new Error(json.errors);
        console.error("[helpers/FetchData] fetching error");
        return json.errors;
    }

    return json ? json : null;
}

export default sendFile;