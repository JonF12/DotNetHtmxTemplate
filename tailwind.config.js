/** @type {import('tailwindcss').Config} */
module.exports = {
    mode: 'jit',
    content: [
        './Views/**/*.cshtml',
        './Views/*.cshtml',
        './Pages/**/*.cshtml',
        './wwwroot/**/*.html'
    ],
    theme: {
        extend: {},
    },
    plugins: [],
}