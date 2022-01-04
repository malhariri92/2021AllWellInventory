import { reactive } from 'vue';

const userState = reactive({
    user: JSON.parse(sessionStorage.getItem('user')),
})

const methods = {
   async login(userName, password){
        const url = 'https://allwellinventory.azurewebsites.net/api/Employee/' + userName + '/' + password;
        
            await fetch(url)
            .then(response => response.json())
            .then(function (data) {
                userState.user = data;
            })
            if (JSON.stringify(userState.user) !== 'false')
            {
                sessionStorage.setItem('user', JSON.stringify(userState.user));
            }
            else 
            {
                userState.user = null;
            }
            return userState.user;
    },

    logout()
    {
        userState.user = null;
        sessionStorage.clear();
    }
}

export default {
    userState,
    methods
}
