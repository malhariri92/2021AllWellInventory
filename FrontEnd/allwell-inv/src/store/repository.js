import { ref } from 'vue';

export function repository() {
    const employee = ref({});

    async function login(userName, password) {
        const url = 'https://localhost:5001/api/Employee/' + userName + '/' + password;

        employee.value = {};

        await fetch(url)
            .then(response => response.json())
            .then(function (data) {
                employee.value = data;
            })

        return employee.value;
    }

    return {
        login
    }
}
