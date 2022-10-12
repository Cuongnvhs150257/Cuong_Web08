<template>
     
     <div class="data-input-button">
         <button type="button" class="content-top-btn1" :tabindex="tab">Cất và Thêm</button>
        <span v-if="toolTip2" class="tool-tip2">
            {{ toolTip2 }}
        </span>
     </div>
</template>

<script>

export default {
    
    props:["tab"],
    
}
</script>

<style>
   
    .content-top-btn1{
    height: 36px;
    padding: 0 24px;
    border: 1px solid #bbbbbb;
    text-align: center;
    background-color: #2CA01C;
    color: #ffff;
    border-radius: 4px;
    cursor: pointer;
    min-width: 100px;
    position: absolute;
    right: 0;
    margin-top: 15px;
    margin-right: 35px;
    font-weight: 700;
    }.content-top-btn1:hover{
        background-color: #35BF22;
    }
    .tool-tip2 {
        position: absolute;
        width: 80px;
        left: 30px;
        top: 20px;
        background-color: #505050;
        border-radius: 2px;
        padding: 2px 4px;
        z-index: 3;
        text-align: center;
        color: #fff;
        visibility: hidden;
        
    }
    .data-input-button:hover .tool-tip2 {
        visibility: visible;
        opacity: 1;
    }
</style>