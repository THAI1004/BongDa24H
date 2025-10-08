import { useAppContext } from "@/context/AppContext";
import { MapPin, Clock, Phone, Users, User } from "lucide-react";
import { useEffect } from "react";
import Loading from "../Loading";

export default function MatchFinderSection() {
    const { matchs, fetchMatch, loading } = useAppContext();
    // console.log(matchs);

    useEffect(() => {
        if (!matchs.length) fetchMatch();
    }, []);

    if (loading && matchs.length === 0) return <Loading />;

    return (
        <section className="border-t border-border/40 bg-muted/30 py-24">
            <div className="max-w-7xl mx-auto container px-6 lg:px-8">
                <div className="mb-12 text-center">
                    <h2 className="mb-4 text-3xl font-bold tracking-tight text-foreground lg:text-4xl">Các Đối Nóng Hổi Hôm Nay</h2>
                    <p className="text-muted-foreground">Tìm đối thủ xứng tầm hoặc đồng đội mới ngay hôm nay</p>
                </div>

                <div className="grid gap-8 md:grid-cols-2 lg:grid-cols-3">
                    {matchs.map((match) => (
                        <div
                            key={match.id}
                            className="group overflow-hidden rounded-2xl border border-border bg-card shadow-sm transition-all hover:border-accent/50 hover:shadow-xl hover:shadow-accent/5"
                        >
                            <div className="p-6">
                                {/* Details */}
                                <div className="space-y-3 text-sm">
                                    <div className="flex items-start gap-3 text-muted-foreground">
                                        <MapPin className="mt-0.5 h-4 w-4 text-accent" />
                                        <span>
                                            <span className="font-semibold text-foreground">{match.pitch?.pitchName}</span> –{" "}
                                            {match.pitch?.cluster?.clusterName}
                                            <br />
                                            <span>{match.pitch?.cluster?.address}</span>
                                        </span>
                                    </div>

                                    <div className="flex items-start gap-3 text-muted-foreground">
                                        <Clock className="mt-0.5 h-4 w-4 text-accent" />
                                        <span>
                                            {match.timeSlot} • {match.matchDate}
                                        </span>
                                    </div>

                                    <div className="flex items-start gap-3">
                                        <Users className="mt-0.5 h-4 w-4 text-accent" />
                                        <span className="font-semibold text-foreground">
                                            {match.needed > 0 ? `Cần ${match.needed} người` : "Tìm đối giao hữu"}
                                        </span>
                                    </div>

                                    <div className="flex items-start gap-3 text-muted-foreground">
                                        <Phone className="mt-0.5 h-4 w-4 text-accent" />
                                        <span>{match.contact || match.creator?.phoneNumber}</span>
                                    </div>

                                    <div className="flex items-start gap-3 text-muted-foreground">
                                        <User className="mt-0.5 h-4 w-4 text-accent" />
                                        <span>Người tạo: {match.creator?.fullName}</span>
                                    </div>
                                </div>

                                {/* CTA Button */}
                                <button className="mt-6 w-full rounded-full border border-accent bg-accent/5 py-3 text-sm font-semibold text-accent transition-all hover:bg-accent hover:text-accent-foreground hover:shadow-lg hover:shadow-accent/20">
                                    Liên Hệ Bắt Đối
                                </button>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </section>
    );
}
