const settingOptions = [
    {
        name: "Personal",
        items: ["Personal Information", "Account and Security"],
        fileName: ["PersonalInformation","AccountAndSecurity"]
    },
    {
        name: "About",
        items: ["About App", "Authors"],
        fileName: ["AboutApp","Authors"]
    }
]

const AccountAndSecurityOptions = [
    {
        name: "General",
        items: ["Change Password"],
        fileName: ["ChangePassword"]
    },
    {
        name: "Critical",
        items: ["Delete Account"],
        fileName:["DeleteAccount"]
    }

]

export {settingOptions, AccountAndSecurityOptions}