const checkEmail = (email) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}
const checkStrim = (string, minLength = 4) => {
    return string.length >= minLength;
}
const checkPhone = (phone) => {
    const phoneRegex = /^(0|\+84)(3[2-9]|5[6|8|9]|7[0|6-9]|8[1-5]|9[0-4|6-9])[0-9]{7}$/;
    return phoneRegex.test(phone);
}
export { checkEmail, checkStrim, checkPhone };