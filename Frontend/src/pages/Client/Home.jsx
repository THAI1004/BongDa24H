import React from "react";
import Banner from "../../components/Home/Banner";
import Introduction from "../../components/Home/Introduction";
import MatchFinderSection from "../../components/Home/MatchFinderSection";
import FieldBookingSection from "../../components/Home/FieldBookingSection";

export default function Home() {
    return (
        <>
            <Banner />
            <Introduction />
            <FieldBookingSection />
            <MatchFinderSection />
        </>
    );
}
