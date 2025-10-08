"use client";
import { ArrowRight } from "lucide-react";
export default function Banner() {
    return (
        <section className=" relative overflow-hidden">
            {/* Background Image with Overlay */}
            <div className=" absolute inset-0">
                <img src="/modern-football-field-aerial-view-sunset.jpg" alt="Football field" className="h-full w-full object-cover" />
                <div className="absolute inset-0 bg-gradient-to-br from-background/95 via-background/80 to-background/60" />
            </div>

            {/* Content */}
            <div className="relative">
                <div className="container mx-auto px-6 py-32 lg:px-8 lg:py-40">
                    <div className="max-w-7xl mx-auto text-cen max-w-3xl">
                        <div className="mb-6 inline-block rounded-full border border-accent/20 bg-accent/10 px-4 py-1.5">
                            <span className="text-sm font-medium text-accent">Nền tảng số 1 Việt Nam</span>
                        </div>

                        <h1 className="mb-6 text-balance text-5xl font-bold leading-tight tracking-tight text-foreground lg:text-7xl">
                            Tìm Sân Dễ Dàng,
                            <br />
                            <span className="text-accent">Giao Hữu Hết Mình</span>
                        </h1>

                        <p className="mb-10 max-w-xl text-pretty text-lg leading-relaxed text-muted-foreground lg:text-xl">
                            Kết nối cộng đồng bóng đá phủi. Đặt sân nhanh chóng, tìm đối dễ dàng, cùng nhau cháy hết mình trên sân cỏ.
                        </p>

                        <div className="flex flex-wrap gap-4">
                            <button className="group inline-flex items-center gap-2 rounded-full bg-accent px-8 py-4 text-base font-semibold text-accent-foreground shadow-lg shadow-accent/20 transition-all hover:bg-accent/90 hover:shadow-xl hover:shadow-accent/30">
                                Đặt Sân Ngay
                                <ArrowRight className="h-5 w-5 transition-transform group-hover:translate-x-1" />
                            </button>

                            <button className="inline-flex items-center gap-2 rounded-full border border-border bg-background/50 px-8 py-4 text-base font-semibold text-foreground backdrop-blur transition-all hover:bg-background hover:shadow-lg">
                                Tìm Đối Thủ
                            </button>
                        </div>
                    </div>
                </div>
            </div>

            {/* Decorative Elements */}
            <div className="absolute bottom-0 left-0 right-0 h-px bg-gradient-to-r from-transparent via-border to-transparent" />
        </section>
    );
}
