import { Button } from "@/components/ui/button";
import { Separator } from "@/components/ui/separator";
import { SidebarTrigger } from "@/components/ui/sidebar";
import { Link, useLocation } from "react-router-dom";
import { data } from "./app-sidebar.jsx";

export function SiteHeader() {
    const { navMain } = data;
    const location = useLocation();
    const currentPath = location.pathname;

    // 🔍 Tìm trong navMain mục có url trùng với đường dẫn hiện tại
    const currentPage = navMain.find((item) => currentPath === item.url);

    // 🔥 Nếu tìm thấy thì lấy title, nếu không thì để mặc định là "Documents"
    const pageTitle = currentPage ? currentPage.title : "Documents";

    return (
        <header className="flex h-(--header-height) shrink-0 items-center gap-2 border-b transition-[width,height] ease-linear group-has-data-[collapsible=icon]/sidebar-wrapper:h-(--header-height)">
            <div className="flex w-full items-center gap-1 px-4 lg:gap-2 lg:px-6">
                <SidebarTrigger className="-ml-1" />
                <Separator orientation="vertical" className="mx-2 data-[orientation=vertical]:h-4" />

                {/* 🧠 Tiêu đề thay đổi theo URL */}
                <h1 className="text-base font-medium">{pageTitle}</h1>

                <div className="ml-auto flex items-center gap-2">
                    <Button variant="ghost" asChild size="sm" className="hidden sm:flex">
                        <Link to="/" rel="noopener noreferrer" target="_blank" className="dark:text-foreground">
                            Trang chủ
                        </Link>
                    </Button>
                </div>
            </div>
        </header>
    );
}
