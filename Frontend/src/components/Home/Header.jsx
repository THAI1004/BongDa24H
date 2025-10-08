import { Link, useNavigate } from "react-router-dom";
import { Search } from "lucide-react";
import { toast } from "sonner";

export default function Header() {
    const user = JSON.parse(localStorage.getItem("user"));
    const navigate = useNavigate();
    const handleLogout = () => {
        // Xóa token trong localStorage/sessionStorage
        localStorage.removeItem("token");
        // Clear context / state
        localStorage.removeItem("user");
        // Có thể redirect về trang chủ
        toast.success("Bạn đã đăng xuất.");

        navigate("/");
    };

    return (
        <header className=" sticky top-0 z-50 border-b border-border/40 bg-background/95 backdrop-blur supports-[backdrop-filter]:bg-background/60">
            <nav className="max-w-7xl container mx-auto px-6 lg:px-8">
                <div className="flex h-16 items-center justify-between">
                    {/* Logo and Navigation */}
                    <div className="flex items-center gap-12">
                        <Link to="/" className="flex items-center">
                            <h1 className="text-2xl font-bold tracking-tight text-foreground">BongDa24H</h1>
                        </Link>

                        <div className="hidden items-center gap-8 md:flex">
                            <Link to="/" className="text-sm font-medium text-foreground transition-colors hover:text-accent">
                                Trang Chủ
                            </Link>
                            <Link to="/batdoi" className="text-sm font-medium text-muted-foreground transition-colors hover:text-foreground">
                                Bắt Đối
                            </Link>
                            <Link to="/tintuc" className="text-sm font-medium text-muted-foreground transition-colors hover:text-foreground">
                                Tin Tức
                            </Link>
                            <Link to="/timsan" className="text-sm font-medium text-muted-foreground transition-colors hover:text-foreground">
                                Tìm Sân
                            </Link>
                        </div>
                    </div>

                    {/* Search and Auth */}
                    <div className="flex items-center gap-4">
                        <div className="relative hidden lg:block">
                            <Search className="absolute left-3 top-1/2 h-4 w-4 -translate-y-1/2 text-muted-foreground" />
                            <input
                                type="text"
                                placeholder="Tìm kiếm sân bóng, đối thủ..."
                                className="h-10 w-64 rounded-full border border-input bg-background pl-10 pr-4 text-sm transition-colors placeholder:text-muted-foreground focus:border-accent focus:outline-none focus:ring-2 focus:ring-accent/20"
                            />
                        </div>
                        {user ? (
                            <div className="flex items-center gap-4">
                                {/* Hiển thị tên user */}
                                <span className="text-sm font-medium text-foreground">
                                    Xin chào, <span className="font-semibold">{user.fullName || user}</span>
                                </span>

                                {/* Nút đăng xuất */}
                                <button
                                    onClick={handleLogout}
                                    className="rounded-full border border-accent bg-accent/5 px-5 py-2 text-sm font-semibold text-accent transition-all hover:bg-accent hover:text-accent-foreground hover:shadow-lg hover:shadow-accent/20"
                                >
                                    Đăng Xuất
                                </button>
                            </div>
                        ) : (
                            <div className="flex items-center gap-4">
                                <Link
                                    to="/login"
                                    className="hidden text-sm font-medium text-muted-foreground transition-colors hover:text-foreground md:block"
                                >
                                    Đăng Nhập
                                </Link>

                                <Link
                                    to="/register"
                                    className="rounded-full bg-accent px-6 py-2 text-sm font-semibold text-accent-foreground transition-all hover:bg-accent/90 hover:shadow-lg hover:shadow-accent/20"
                                >
                                    Đăng Ký
                                </Link>
                            </div>
                        )}
                    </div>
                </div>
            </nav>
        </header>
    );
}
