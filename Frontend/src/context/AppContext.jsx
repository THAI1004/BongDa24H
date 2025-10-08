import axios from "axios";
import React, { createContext, useContext, useState } from "react";

const AppContext = createContext();
export default function AppProvider({ children }) {
    const [fiels, setFiel] = useState([]);
    const [matchs, setMatch] = useState([]);
    const [booking, setBooking] = useState([]);
    const [team, setTeam] = useState([]);
    const [loading, setLoading] = useState(false);
    const BEURL = import.meta.env.VITE_BEURL;
    const token = localStorage.getItem("token"); // hoặc lấy từ state/context
    const fetchField = async () => {
        setLoading(true);
        try {
            const response = await axios.get(`${BEURL}/pitchcluster`);
            setFiel(response.data?.data);
            setLoading(false);
        } catch (error) {
            console.error("Lỗi khi lấy dữ liệu sân bóng:", error);
        }
    };
    const fetchMatch = async () => {
        setLoading(true);
        try {
            const response = await axios.get(`${BEURL}/match-request`);
            setMatch(response?.data?.data);
            setLoading(false);
        } catch (error) {
            console.error("Lỗi khi lấy dữ liệu đối:", error);
        }
    };
    return (
        <AppContext.Provider
            value={{
                fiels,
                matchs,
                loading,
                fetchField,
                fetchMatch,
            }}
        >
            {children}
        </AppContext.Provider>
    );
}
export function useAppContext() {
    return useContext(AppContext);
}
