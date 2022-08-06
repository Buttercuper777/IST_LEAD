import Logo from "./Logo";
import styles from "../styles/headerStls.module.css"

export default function MainHeader(){
    return(
          <header className={styles.headerCont}>
              <div className={"container-fluid"}>
                  <div className={
                      "row d-flex justify-content-center"
                  }>
                      <div className={"col-lg-8"}>
                          <div className={"col-lg-3"}>
                            <Logo/>
                          </div>
                      </div>
                  </div>
              </div>
          </header>
    );
}