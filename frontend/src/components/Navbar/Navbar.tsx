import "./Navbar.css";

import {
    MAIN_ROUTE
} from "../../app/constants";

type NavbarType = {
    name: string;
    url: string;
}

export function Navbar() {
    const navbarItems: NavbarType[] = [
        { name: "Home", url: MAIN_ROUTE },
        { name: "Dashboard", url: "/dashboard" }
    ]

    return (
        <nav className="navbar navbar-expand-md navbar-dark linker_navbar mb-4">
            <div className="container-fluid linker_navbar-wrapper">
                <a className="navbar-brand" href="#">
                    Linker
                </a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarCollapse" aria-controls="navbarCollapse" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarCollapse">
                    <ul className="navbar-nav me-auto mb-2 mb-md-0">
                        {
                            navbarItems.map((v, i) => {
                                return (
                                    <li className="nav-item" key={i}>
                                        <a className="nav-link linker_navbar_nav-link" aria-current="page" href={v.url}>{v.name}</a>
                                    </li>            
                                );
                            })
                        }
                    </ul>
                </div>
            </div>
        </nav>
    )
}
