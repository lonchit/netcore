<template>
    <div>
        <h2>Новости</h2>
        <ul>
            <li v-for="value in pagedListOfNewsListDto">
                <div class="news-item">
                    <hr>
                    <div class="title">
                        {{ value.title }}
                    </div>

                    <div class="description">
                        {{ value.description}}
                    </div>

                </div>

            </li>
            <hr>
        </ul>
    </div>
</template>

<script src="./news-list.ts"></script>
<style src="./news-list.css"></style>