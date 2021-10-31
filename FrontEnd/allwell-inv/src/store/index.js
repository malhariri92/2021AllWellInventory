import { reactive } from 'vue';

const userState = reactive({
    user: JSON.parse(sessionStorage.getItem('user')),
})

const methods = {
   async login(userName, password){
        const url = 'https://localhost:44364/api/Employee/' + userName + '/' + password;
        
            await fetch(url)
            .then(response => response.json())
            .then(function (data) {
                userState.user = data;
            })
            sessionStorage.setItem('user', JSON.stringify(userState.user));
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
