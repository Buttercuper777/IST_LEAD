export default function Logo(){
    return(
        <>
            <div style={{
                width : 144 + "px",
                height: "max-content",
                display: "flex",
                flexDirection: "column",
            }}>
                <a style={{
                    fontSize: 36 + "px",
                    fontWeight: "600",
                    color: "#EDEBFB",
                    textDecoration: "none",
                    fontFamily: "Inter",

                }}>
                    LEAD.
                </a>
                <a style={{
                    fontSize: 20 + "px",
                    fontWeight: "600",
                    color: "#B7B1F1",
                    textDecoration: "none",
                    fontFamily: "Inter",
                    lineHeight: 14 + "px"
                }}>
                    IST ELEVATOR
                </a>
            </div>
        </>
    )
}