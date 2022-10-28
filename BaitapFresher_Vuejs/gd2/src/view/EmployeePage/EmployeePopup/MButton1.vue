<template>
     
     <div class="data-input-button">
         <button type="button" class="content-top-btn1" :tabindex="tab">Cất và Thêm</button>
        <span v-if="toolTip" class="tool-tip">
            {{ toolTip }}
        </span>
     </div>
</template>

<script>

export default {
    
    props:["toolTip","tab"],
    
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
    }.data-input-button:hover .tool-tip {
        visibility: visible;
        opacity: 1;
        left: 750px;
        top: 85px;
        font-size: 12px;
    }
</style>