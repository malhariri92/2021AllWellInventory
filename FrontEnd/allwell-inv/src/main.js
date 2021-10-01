import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { library } from '@fortawesome/fontawesome-svg-core'
import { fas } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

library.add(fas)
const app = createApp(App).use(router);

app.component('font-awesome-icon', FontAwesomeIcon);
app.config.globalProperties.$filters = {
  shortDate(date) {
      if (!date) {
          return 'N/A'
      }
  
      return new Date(date).toLocaleDateString('en-US', { day: '2-digit', month: '2-digit', year: 'numeric'});
  }
}

app.mount('#app');