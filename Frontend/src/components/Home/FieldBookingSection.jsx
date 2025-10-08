"use client";
import { MapPin, Clock } from "lucide-react";
import { useEffect } from "react";
import Loading from "../Loading.jsx";
import { useAppContext } from "@/context/AppContext.jsx";

export default function FieldBookingSection() {
    const { fiels, fetchField, loading } = useAppContext();
    useEffect(() => {
        if (!fiels.lenght) fetchField();
    }, []);
    if (loading && fiels.length === 0) return <Loading />;
    return (
        <section className="max-w-7xl mx-auto py-24">
            <div className="container mx-auto px-6 lg:px-8">
                <div className="mb-12 text-center">
                    <h2 className="mb-4 text-balance text-3xl font-bold tracking-tight text-foreground lg:text-4xl">Các cụm sân nổi bật</h2>
                    <p className="text-pretty text-muted-foreground">Đặt ngay những khung giờ đẹp nhất trong ngày</p>
                </div>

                <div className="grid gap-8 md:grid-cols-2 lg:grid-cols-3">
                    {fiels.map((field) => (
                        <div
                            key={field.id}
                            className="group overflow-hidden rounded-2xl border border-border bg-card shadow-sm transition-all hover:shadow-xl hover:shadow-accent/5"
                        >
                            {/* Image */}
                            <div className="relative aspect-[4/3] overflow-hidden bg-muted">
                                <img
                                    src={field.image || "/modern-football-field-5v5.jpg"}
                                    alt={field.name}
                                    className="h-full w-full object-cover transition-transform duration-500 group-hover:scale-105"
                                />
                                <div className="absolute right-4 top-4 rounded-full bg-background/90 px-3 py-1 text-sm font-semibold text-foreground backdrop-blur">
                                    {field.type}
                                </div>
                            </div>

                            {/* Content */}
                            <div className="p-6">
                                <h3 className="mb-2 text-xl font-bold text-card-foreground">{field.name}</h3>

                                <div className="mb-4 flex items-start gap-2 text-sm text-muted-foreground">
                                    <MapPin className="mt-0.5 h-4 w-4 flex-shrink-0" />
                                    <span>{field.address}</span>
                                </div>
                                {/* CTA Button */}
                                <button className="w-full rounded-full bg-accent py-3 text-sm font-semibold text-accent-foreground transition-all hover:bg-accent/90 hover:shadow-lg hover:shadow-accent/20">
                                    Đặt Sân Này
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
}
