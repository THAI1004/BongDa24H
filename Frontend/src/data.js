export const mockMatches = [
    { id: 1, teamName: "FC Anh Em", field: "Sân Hoàng Mai", time: "19:00 - 20:30", needed: 5, contact: "Anh Long" },
    { id: 2, teamName: "Old Boys FC", field: "Sân C500", time: "20:30 - 22:00", needed: 0, contact: "Anh Tú" }, // needed: 0 nghĩa là tìm đối
    { id: 3, teamName: "Student United", field: "Sân Bách Khoa", time: "17:30 - 19:00", needed: 3, contact: "Việt" },
];

export const mockFields = [
    {
        id: 1,
        name: "Sân bóng đá Mỹ Đình 2",
        address: "Lê Quang Đạo, Nam Từ Liêm",
        type: "Sân 7 người",
        price: "500k-800k",
        availableSlots: ["17:30", "19:00", "20:30"],
    },
    { id: 2, name: "Sân bóng Cầu Giấy", address: "Trần Quốc Hoàn, Cầu Giấy", type: "Sân 11 người", price: "1tr-1.5tr", availableSlots: ["20:30"] },
    {
        id: 3,
        name: "Sân bóng PVV",
        address: "Hoàng Quốc Việt, Bắc Từ Liêm",
        type: "Sân 7 người",
        price: "450k-700k",
        availableSlots: ["19:00", "20:30"],
    },
];
