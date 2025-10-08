// src/components/ui/sonner.tsx
import { Toaster as Sonner } from "sonner";

export function Toaster(props) {
    return <Sonner position="top-center" richColors {...props} />;
}
