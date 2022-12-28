<template>
    <div class="cover_toast" v-if="text">
        <div class="toast">
        <div :class="classcssicon"></div>
        <div class="toast_text">
            <div :class="classcss">{{text_color}}</div>
            <div class="toast_text_noti">{{text}}</div>
        </div>
    </div>
    </div>
    
</template>

<script>
export default {
    props:{
        text: String,
        text_color: String,
        classcss: String,
        classcssicon:String
    },
    
    methods:{
        
    },
    data(){
        return{
            
        }
    }
}
</script>

<style>
    :root {
        --icon: url("../../../assets/Resource/img/Sprites.64af8f61.svg");
    }

    .cover_toast{
        position: absolute;
        bottom: 10px;
        right: 15px;
    }
    .toast{
        height: 48px;
        width: 300px;
        z-index: 5;
        border-radius: 4px;
        box-shadow: 0px 3px 6px rgba(0, 0, 0, 0.16);
        background-color: #fff;
        line-height: 48px;
        padding-left: 10px;
        position: relative;
        animation: slideInLeft ease 0.7s, fadeOut linear 1s 3s forwards;
    }.toast_icon-success{
        position: absolute;
        top: 15px;
        left: 15px;
        width: 24px;
        height: 24px;
        background-image: var(--icon);
        background-position: -848px -312px;
        background-repeat: no-repeat;
    }.toast_icon_failed{
        position: absolute;
        top: 15px;
        left: 15px;
        width: 24px;
        height: 24px;
        background-image: var(--icon);
        background-position: -1552px -312px;
        background-repeat: no-repeat;
    }.toast_text{
        display: flex;
    }
    .toast_text_color-success{
        padding-left: 35px;
        width: 90px;
        color: #50B83C;
        font-size: 14px;
        font-weight: 900;
    }
    .toast_text_color-failed{
        padding-left: 35px;
        width: 90px;
        color: #da2121;
        font-size: 14px;
        font-weight: 900;
    }
    .toast_tex-noti{
        padding-left: 100px;
        
    }.toast_button{
        position: absolute;
        top: 15px;
        right: 15px;
        width: 24px;
        height: 24px;
        background-image: var(--icon);
        background-position: -147px -147px;
        background-repeat: no-repeat;
    }
    @keyframes slideInLeft {
    from {
        transform: translateY(calc(-60px));
    }
    to {
        opacity: 1;
        transform: translateY(0px);
    }
    }

    @keyframes fadeOut {
    from {
        transform: translateY(0px);
    }
    to {
        opacity: 0;
        transform: translateY(calc(-60px));
    }
}
</style>