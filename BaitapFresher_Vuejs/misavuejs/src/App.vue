<template>
    <div class="container">
      <the-header class="header"></the-header>
      <the-sidebar class="sidebar"></the-sidebar>
      <the-content class="content"></the-content>
    </div>
    
</template>

<script>
import TheHeader from './components/TheHeader/TheHeader.vue'
import TheSidebar from './components/TheSidebar/TheSidebar.vue'
import TheContent from './components/TheContent/TheContent.vue'

export default {
  name: 'App',
  components: {
    
    TheHeader,
    TheSidebar,
    TheContent
  }
}
</script>

<style>
  .container{
    font-family: Misa Fonts Regular;
    font-size: 14px;
    width: 100%;
    height: 100vh;
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;

}
*{
    margin: 0;
    padding: 0;

}
@font-face {
    font-family: Misa Fonts Regular;
    src: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\fonts\notosans-regular.2cb88a13.woff2);
}
@font-face {
    font-family: Misa Fonts Bold;
    src: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\fonts\notosans-bold.3ede5aff.woff2);
}
@font-face {
    font-family: Misa Fonts Italic;
    src: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\fonts\notosans-italic.1c1aaac9.woff2);
}
@font-face {
    font-family: Misa Fonts Semibold;
    src: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\fonts\notosans-semibold.df3e6dc4.woff2);
}
.header{
    order: 2;
    width: calc(100%- 200px);
    height: 50px;
    float: left;
    clear: right;
  

}
.sidebar{
    width: 200px;
    height: 100vh;
    background-color: #041434;
    
   
}
.content{
    order: 3;
    width: calc(100% - 200px);
    height: calc(100vh - 50px);
    float: right;
    background-color: rgb(236, 238, 241);
}
</style>
