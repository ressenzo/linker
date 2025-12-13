import { Navbar } from "../../components/Navbar/Navbar";
import { Outlet } from "react-router-dom";
import "./Wrapper.css";

export function Wrapper() {
    return (
        <>
            <Navbar />
            <div className="container linker_wrapper">
                <main className="linker_wrapper-main  mt-4">
                    <Outlet />
                </main >
            </div>
        </>
    )
}
