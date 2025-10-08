import { Link } from "react-router-dom";
import { Facebook, Youtube } from "lucide-react";

export default function Footer() {
    return (
        <footer className="max-w-7xl mx-auto border-t border-border/40 bg-background">
            <div className="container mx-auto px-6 py-16 lg:px-8">
                <div className="grid gap-12 md:grid-cols-2 lg:grid-cols-4">
                    {/* Brand */}
                    <div className="lg:col-span-1">
                        <h2 className="mb-4 text-2xl font-bold tracking-tight text-foreground">BongDa24H</h2>
                        <p className="text-pretty text-sm leading-relaxed text-muted-foreground">
                            Nền tảng đặt sân và kết nối cộng đồng bóng đá phủi hàng đầu, mang sân cỏ đến gần bạn hơn.
                        </p>
                    </div>

                    {/* Links */}
                    <div>
                        <h3 className="mb-4 text-sm font-semibold text-foreground">Liên kết</h3>
                        <ul className="space-y-3 text-sm">
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Về chúng tôi
                                </Link>
                            </li>
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Tin tức & Sự kiện
                                </Link>
                            </li>
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Đối tác sân bóng
                                </Link>
                            </li>
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Liên hệ
                                </Link>
                            </li>
                        </ul>
                    </div>

                    {/* Support */}
                    <div>
                        <h3 className="mb-4 text-sm font-semibold text-foreground">Hỗ trợ</h3>
                        <ul className="space-y-3 text-sm">
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Câu hỏi thường gặp
                                </Link>
                            </li>
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Điều khoản dịch vụ
                                </Link>
                            </li>
                            <li>
                                <Link href="#" className="text-muted-foreground transition-colors hover:text-accent">
                                    Chính sách bảo mật
                                </Link>
                            </li>
                        </ul>
                    </div>

                    {/* Social */}
                    <div>
                        <h3 className="mb-4 text-sm font-semibold text-foreground">Theo dõi chúng tôi</h3>
                        <div className="flex gap-3">
                            <Link
                                href="#"
                                className="flex h-10 w-10 items-center justify-center rounded-full border border-border bg-background transition-all hover:border-accent hover:bg-accent hover:text-accent-foreground"
                            >
                                <Facebook className="h-5 w-5" />
                            </Link>
                            <Link
                                href="#"
                                className="flex h-10 w-10 items-center justify-center rounded-full border border-border bg-background transition-all hover:border-accent hover:bg-accent hover:text-accent-foreground"
                            >
                                <Youtube className="h-5 w-5" />
                            </Link>
                        </div>
                    </div>
                </div>

                {/* Bottom */}
                <div className="mt-12 border-t border-border/40 pt-8 text-center">
                    <p className="text-sm text-muted-foreground">&copy; {new Date().getFullYear()} BongDa24H. All rights reserved.</p>
                </div>
            </div>
        </footer>
    );
}
