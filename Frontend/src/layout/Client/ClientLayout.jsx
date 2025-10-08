import React from "react";
import Footer from "../../components/Home/Footer";
import { Outlet } from "react-router-dom";
import Header from "@/components/Home/Header.jsx";
import AppProvider from "@/context/AppContext";

export default function ClientLayout() {
    return (
        <AppProvider>
            <div className="    font-sans">
                <div className="">
                    <Header />
                    <main>
                        <Outlet />
                    </main>
                    <Footer />
                </div>
            </div>
        </AppProvider>
    );
}
