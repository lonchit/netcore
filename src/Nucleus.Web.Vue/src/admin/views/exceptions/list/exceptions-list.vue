<template>
    <div>
        <h2>Ошибки</h2>
        <ul>
            <li v-for="value in pagedListOfExceptionsListDto">
                <div class="news-item">
                    <hr>
                    <div class="title">
                        {{ value.text }}
                    </div>
                    <div class="datetime">
                        {{ value.creationDate }}
                    </div>
                </div>

            </li>
            <hr>
        </ul>
    </div>
</template>

<script src="./exceptions-list.ts"></script>