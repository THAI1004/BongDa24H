"use client";
import { MapPin, Clock, Star, Users, Zap } from "lucide-react";
import { useEffect } from "react";
import Loading from "../Loading.jsx";
import { useAppContext } from "@/context/AppContext.jsx";

export default function FieldBookingSection() {
    const { fiels, fetchField, loading } = useAppContext();

    console.log("🚀 ~ FieldBookingSection ~ fiels:", fiels);

    useEffect(() => {
        if (!fiels.lenght) fetchField();
    }, []);
    if (loading && fiels.length === 0) return <Loading />;
    return (
        <section className="max-w-7xl mx-auto py-24">
            <div className="container mx-auto px-6 lg:px-8">
                <div className="mb-16 text-center">
                    <div className="mb-3 inline-flex items-center gap-2 rounded-full bg-accent/10 px-4 py-1.5 text-sm font-semibold text-accent">
                        <Zap className="h-4 w-4" />
                        <span>Sân Chất Lượng Cao</span>
                    </div>
                    <h2 className="mb-4 text-balance text-4xl font-bold tracking-tight text-foreground lg:text-5xl">Các cụm sân nổi bật</h2>
                    <p className="text-pretty text-lg text-muted-foreground">Đặt ngay những khung giờ đẹp nhất trong ngày</p>
                </div>

                <div className="grid gap-8 md:grid-cols-2 lg:grid-cols-3">
                    {fiels.map((field) => (
                        <div
                            key={field.id}
                            className="group relative overflow-hidden rounded-3xl border border-border bg-card shadow-lg transition-all duration-500 hover:-translate-y-2 hover:shadow-2xl hover:shadow-accent/10"
                        >
                            <div className="relative aspect-[4/3] overflow-hidden bg-muted">
                                <img
                                    src={field.image || "/modern-football-field-5v5.jpg"}
                                    alt={field.clusterName}
                                    className="h-full w-full object-cover transition-transform duration-700 group-hover:scale-110"
                                />
                                {/* Gradient overlay for better text readability */}
                                <div className="absolute inset-0 bg-gradient-to-t from-black/60 via-black/0 to-black/20" />

                               
                            </div>

                            <div className="p-6">
                                <div className="mb-4">
                                    <h3 className="mb-2 text-2xl font-bold tracking-tight text-card-foreground">{field.clusterName}</h3>

                                    <div className="mb-3 flex items-start gap-2 text-sm text-muted-foreground">
                                        <MapPin className="mt-0.5 h-4 w-4 flex-shrink-0 text-accent" />
                                        <span className="line-clamp-2">{field.address}</span>
                                    </div>
                                </div>

                                <div className="mb-4 flex items-center gap-4 border-t border-border pt-4">
                                    <div className="flex items-center gap-1.5 text-sm text-muted-foreground">
                                        <Clock className="h-4 w-4 text-accent" />
                                        <span>24/7</span>
                                    </div>
                                    <div className="flex items-center gap-1.5 text-sm text-muted-foreground">
                                        <Users className="h-4 w-4 text-accent" />
                                        <span>{field.pitches.pitchType ==1 ? "5x5" :"7x7 "}</span>
                                    </div>
                                </div>

                                <div className="flex items-center justify-between gap-3">
                                    <div>
                                        <div className="text-sm text-muted-foreground">Từ</div>
                                        <div className="text-2xl font-bold text-accent">
                                            300K<span className="text-base font-normal text-muted-foreground">/giờ</span>
                                        </div>
                                    </div>

                                    <button className="group/btn relative overflow-hidden rounded-full bg-accent px-6 py-3 text-sm font-bold text-accent-foreground shadow-lg shadow-accent/20 transition-all duration-300 hover:scale-105 hover:shadow-xl hover:shadow-accent/30">
                                        <span className="relative z-10">Đặt Sân Ngay</span>
                                        {/* Shimmer effect on hover */}
                                        <div className="absolute inset-0 -translate-x-full bg-gradient-to-r from-transparent via-white/20 to-transparent transition-transform duration-700 group-hover/btn:translate-x-full" />
                                    </button>
                                </div>
                            </div>

                            <div className="absolute inset-0 rounded-3xl border-2 border-accent/0 transition-colors duration-500 group-hover:border-accent/20" />
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
}
