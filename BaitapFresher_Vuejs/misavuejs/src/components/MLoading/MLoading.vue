<template>
    <div class="cover-loading">

        <div class="loading-icon icon"></div>

    </div>
</template>

<style>
    .cover-loading{
        position: fixed;
        width: 100%;
        height: 100vh;
        right: 0;
        left: 0;
        top: 0;
        bottom: 0;
        background-color: rgba(12, 12, 12, 0.3);
        z-index: 1000;
    }.loading-icon{
        width: 300px;
        height: 300px;
        position: fixed;
        top: 50%;
        right: 25%;
    }
    .icon{
        background-image: url(C:\Users\s\Documents\GitHub\Cuong_Web08\BaitapFresher_Vuejs\misavuejs\src\assets\Resource\loading.svg);
        background-repeat: no-repeat;
        background-size: 50px;
    }
</style>