/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./app/**/*.{js,jsx,ts,tsx}", "./components/**/*.{js,jsx,ts,tsx}"],
  theme: {
    extend: {
      colors: {
        primary: {
          DEFAULT: "#2C3E50",
          100: "#b4c7dd",
          200: "#57687c"},
        secondary: {
          DEFAULT: "#926b6a",
          100: "#F7CAC9",
        },
        black: {
          DEFAULT: "#333333",
        },
        gray: {
          DEFAULT:"#5c5c5c",
          100: "#bfbfbf",
        },
        white: {
          DEFAULT:"#F2F2F2",
          100: "#e8e8e8"
        }
      },
      fontFamily: {
        pthin: ["Poppins-Thin", "sans-serif"],
        pextralight: ["Poppins-ExtraLight", "sans-serif"],
        plight: ["Poppins-Light", "sans-serif"],
        pregular: ["Poppins-Regular", "sans-serif"],
        pmedium: ["Poppins-Medium", "sans-serif"],
        psemibold: ["Poppins-SemiBold", "sans-serif"],
        pbold: ["Poppins-Bold", "sans-serif"],
        pextrabold: ["Poppins-ExtraBold", "sans-serif"],
        pblack: ["Poppins-Black", "sans-serif"],
      },
    },
  },
  plugins: [],
};