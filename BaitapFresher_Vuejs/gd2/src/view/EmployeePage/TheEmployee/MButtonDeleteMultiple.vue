<template>
    <div class="deletemultple">
        <div class="deletemultple-btn" @click="handleDeleteMultiple">
            <div class="deletemultple-lable"><label >Thực hiện xóa hàng loạt</label></div>
            <div class="deletemultple-icon"></div>
        </div>
    </div>
</template>

<script>
export default {
    methods:{
        /**
         * Hàm bấm xóa nhiều
         * Nguyễn Văn Cương 15/10/2022
         */
        handleDeleteMultiple(){
            this.$emit("handle-deletemu");
        }
    }
}
</script>

<style>
    :root {
        --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
    }
    .deletemultple{
        width: 200px;
        height: 40px;
        border: 2px solid #000;
        border-radius: 30px;
        display: flex;
        justify-content: center;
        position: relative;
        cursor: pointer;
    }.deletemultple:hover{
        background-color: rgb(236, 238, 241);
    }
    .deletemultple-lable{
        margin-top: 11px;
        margin-right: 25px;
        font-size: 13px;
        font-family: Misa Fonts Semibold;
        cursor: pointer;
    }.deletemultple-lable label{
        cursor: pointer;
    }
    .deletemultple-icon{
        width: 24px;
        height: 24px;
        background-image: var(--icon);
        background-position: -561px -357px;
        background-repeat: no-repeat;
        position: absolute;
        top: 10px;
        right: 0;
        cursor: pointer;
    }
</style>