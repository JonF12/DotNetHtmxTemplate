/** @type {import('tailwindcss').Config} */
module.exports = {
    mode: 'jit',
    content: [
        './Views/**/*.cshtml',
        './Views/*.cshtml',
        './Pages/**/*.cshtml',
        './Pages/*.cshtml',
        './wwwroot/**/*.html'
    ],
    theme: {
        extend: {
            fontFamily: {
                sans: ['Open Sans', 'ui-sans-serif', 'system-ui', '-apple-system', 'sans-serif'],
            },
        },
    },
    plugins: [],
}