<template>
    <div v-if="!creationNewsIsComplete">
        <v-card class="elevation-12">
            <v-toolbar dark color="primary">
                <v-toolbar-title>{{$t('CreateNews')}}</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <div v-for="error in errors" :key="error.name">
                    <v-alert :value="true" type="error">
                        {{$t(error.name)}}
                    </v-alert>
                </div>
                <v-form ref="form" @keyup.native.enter="onSubmit">
                    <v-text-field name="title" :label="$t('Title')" type="text"
                                  v-model="createNewsInput.title"></v-text-field>
                    <v-text-field name="Description" :label="$t('Description')" type="text"
                                  v-model="createNewsInput.description"></v-text-field>
                </v-form>
            </v-card-text>
            <v-card-actions class="pa-5">
                <v-spacer></v-spacer>
                <v-btn color="primary" @click="onSubmit">{{$t('CreateNews')}}</v-btn>
            </v-card-actions>
        </v-card>
    </div>
    <div v-else>
        <v-card class="elevation-12">
            <v-toolbar dark color="primary">
                <v-toolbar-title>{{$t('CreateNews')}}</v-toolbar-title>
            </v-toolbar>
            <v-card-text>
                <v-alert :value="true" type="success">
                    {{resultMessage}}
                </v-alert>
            </v-card-text>
        </v-card>
    </div>
</template>

<script src="./create-news.ts"></script>
<style src="./create-news.css"></style>