<template>
    <div class="cover-popup">

        <div class="popupask">
            <div class="popupask-top">
                <div class="popupask-icon icon-ask"></div>
                <div class="popupask-icon-label">
                    <label class="label-ask">Bạn có muốn xóa nhân viên này không?</label>
                </div>
                <button class="popupask-icon2 icon-cance" @click="popupaskcance"></button>
                
            </div>
            <div class="popupask-bottom">
                <MButtonAgree @click="agreedelete"/>
            </div>
        </div>

    </div>
</template>

<script>
import MButtonAgree from './MButtonAgree.vue'

export default ({

    methods: {

    //hàm nhấn nút hủy xóa nhân viên
    popupaskcance(){
        this.$emit("popup-ask-cance");
    },
    //hàm đồng ý xóa nhân viên
    agreedelete() {
      this.$emit("agree-delete-click");
    },
  },
    components: { MButtonAgree},
})
</script>

<style>

    :root{
         --icon-popupask: url("../../assets/Resource/img/Sprites.64af8f61.svg");
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
    }.popupask{
        width: 500px;
        height: 200px;
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
        height: 50px;
        margin-left: 50px;
        margin-top: 50px;
        border-bottom: 1px solid;
    }.icon-ask{
        background-image: var(--icon-popupask);
        background-position: -598px -463px;
        background-repeat: no-repeat;
        background-size: 30px;
    }.popupask-icon-label{
        width: 350px;
        height: 50px;
        text-align: center;
        margin-top: 50px;
        border-bottom: 1px solid;
    }.label-ask{
        color: #6B6B6B;
    }.popupask-icon2{
        width: 50px;
        height: 50px;
    }.icon-cance{
        background-image: var(--icon-popupask);
        background-position: -931px -147px;
        background-repeat: no-repeat;
        background-size: 30px;
    }
</style>


