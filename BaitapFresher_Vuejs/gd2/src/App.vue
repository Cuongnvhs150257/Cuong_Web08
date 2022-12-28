<template>
    <div id="page-employee" class="container">
      
      <the-header class="header"></the-header>
      <the-sidebar class="sidebar"></the-sidebar>
      <the-content class="content"></the-content>
    </div>

</template>

<script>
import TheHeader from './components/Layout/TheHeader/TheHeader.vue'
import TheSidebar from './components/Layout/TheSidebar/TheSidebar.vue'
import TheContent from './components/Layout/TheContent/TheContent.vue'

export default {
  name: 'MisaPage',
  components: {
    
    TheHeader,
    TheSidebar,
    TheContent,
  },
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
    background-color: #ECEEF1;

}
*{
    margin: 0;
    padding: 0;

}
@font-face {
    font-family: Misa Fonts Regular;
    src: url('./assets/Resource/fonts/notosans-regular.2cb88a13.woff2');
}
@font-face {
    font-family: Misa Fonts Bold;
    src: url('./assets/Resource/fonts/notosans-bold.3ede5aff.woff2');
}
@font-face {
    font-family: Misa Fonts Italic;
    src: url('./assets/Resource/fonts/notosans-italic.1c1aaac9.woff2');
}
@font-face {
    font-family: Misa Fonts Semibold;
    src: url('./assets/Resource/fonts/notosans-semibold.df3e6dc4.woff2');
}
.header{
    order: 2;
    height: 56px;
    float: left;
    clear: right;
    background-color: #ffff;
  

}
.sidebar{
    width: 180px;
    height: 100vh;
    background-color: #393A3D;
    
   
}
.content{
    order: 3;
    width: calc(100% - 230px);
    height: calc(100vh - 88px);
    float: right;
    background-color: rgb(236, 238, 241);
    padding: 0 24px;
    padding-top: 24px;
    position: relative;
}
.popup{
    position: fixed;
    width: 100%;
    height: 100vh;
    right: 0;
    left: 0;
    top: 0;
    bottom: 0;
    background-color: rgba(12, 12, 12, 0.6);
}

@media (max-width: 1200px) {
    .sidebar{
    width: 50px;
    
    }.content{
        width: calc(100% - 100px);
    }
}
</style>

