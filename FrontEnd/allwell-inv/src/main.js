import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

const app = createApp(App).use(router);

app.config.globalProperties.$filters = {
  shortDate(date) {
      if (!date) {
          return 'N/A'
      }
  
      return new Date(date).toLocaleDateString('en-US', { day: '2-digit', month: '2-digit', year: 'numeric'});
  }
}

app.mount('#app');