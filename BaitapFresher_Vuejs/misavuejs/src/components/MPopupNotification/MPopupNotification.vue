<template>
    <div class="cover-popup">

        <div class="popupnoti">
            <div class="popupask-top">
                <div class="popupask-icon icon-noti"></div>
                <div class="popupask-icon-label">
                    <label class="label-ask">{{errors}}</label>
                </div>

            </div>
            <div class="popupask-bottom">
                <MButtonClose @click="closeNotification"/>
            </div>
        </div>

    </div>
</template>

<script>
import MButtonClose from './MButtonClose.vue'

export default ({
    props:["errors"],
    methods: {


    //hàm đồng ý xóa nhân viên
    closeNotification() {
      this.$emit("close-notification-click");
    },
  },
    components: { MButtonClose},
})
</script>

<style>

    :root{
         --icon: url("../../assets/Resource/img/Sprites.64af8f61.svg");
    }
    .cover-popup{
        position: fixed;
        width: 100%;
        height: 100vh;
        right: 0;
        left: 0;
        top: 0;
        bottom: 0;
        background-color: rgba(12, 12, 12, 0.3);
        z-index: 1000;
    }.popupnoti{
        width: 450px;
        height: 180px;
        position: relative;
        top: 40%;
        margin: 0 auto;
        background-color: #fff;
    }.popupask-top{
        width: 100%;
        height: 100px;
        display: flex;
        position: relative;
        
    }.popupask-icon{
        width: 50px;
        height: 49px;
        margin-left: 50px;
        margin-top: 50px;
        border-bottom: 1px solid #bbbb;
    }.icon-noti{
        background-image: var(--icon);
        background-position: -752px -462px;
        background-repeat: no-repeat;

    }.popupask-icon-label{
        width: 270px;
        margin-top: 50px;
        border-bottom: 1px solid #bbbb;
        padding-top: 10px;
    }.label-ask{
        color: #000;
    }.popupask-icon2{
        width: 50px;
        height: 50px;
        background-color: #fff;
        border: none;
    }.icon-cance{
        background-image: var(--icon);
        background-position: -128px -131px;
        background-repeat: no-repeat;
        
    }
</style>


